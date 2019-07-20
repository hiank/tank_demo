using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Moba.Views;

namespace Moba.Logic
{
    [Serializable]
    public class Meet
    {

        public TankManager[] m_Tanks;

        //public Item Hero { set; get; }
        private Scene fieldScene;
        private PhysicsScene physicField;
        private GameObject planePrefab;
        private GameObject plane;


        private List<WarPb.Tick> localTicks;        //NOTE: 本地在上一个NetTick 所以以后产生的Tick 队列
        private List<WarPb.Tick> laterTicks;        //NOTE: 延迟的tick 队列
        private List<ItemInfo> netShot;                //NOTE: 快照信息[Net Tick 产生的最新的场景状态]
        private bool shotUpdated = true;
        //private Views.Item copyRole;

        private int netTickIndex = 0;               //NOTE: 当前的服务器演算帧索引

        private int leftSimularTimes = 0;

        private WarPb.Tick curTick = null;
        private int fixedIdx = 0;
        //private Dictionary<ulong, int> tagDic;     //NOTE: uid 转场景tag 
        //private Dictionary<ulong, Item> itemMap; //NOTE: 通过uid 获得Item

        public TankManager GetTM(ulong uid)
        {
            TankManager tm = null;
            foreach (var tmp in m_Tanks)
            {
                if (uid == tmp.m_Uid)
                {

                    tm = tmp;
                    break;
                }
            }
            return tm;
        }

        public Meet()
        {
            localTicks = new List<WarPb.Tick>();
            laterTicks = new List<WarPb.Tick>();
            netShot = new List<ItemInfo>();
            //tagDic = new Dictionary<ulong, int>();
            //itemMap = new Dictionary<ulong, Item>();
        }

        public void DoMatch(WarPb.War_Match data)
        {
            //data.MapId      //NOTE: 用于确定使用哪个地图
            //DoMatch(data, SceneManager.CreateScene("PhysicField"));

            //Scene scene = SceneManager.GetActiveScene();

            int idx = 0;
            foreach (WarPb.Team team in data.League)
            {
                foreach (MasterPb.Role role in team.Roles)
                {
                    TankManager tm = m_Tanks[idx];
                    tm.m_Instance = GameObject.Instantiate(GetPrefab(role.ModelId), tm.m_SpawnPoint.position, tm.m_SpawnPoint.rotation) as GameObject;
                    //SceneManager.MoveGameObjectToScene(tm.m_Instance, scene);
                    tm.m_Uid = role.Uid;
                    tm.Setup();
                    idx++;
                }
            }
            Physics.IgnoreLayerCollision(9, 9);
        }

        //DoTick 处理tick，将对演示状态进行一定修正，所有的碰撞都在此处发生响应
        public void DoNetTick(WarPb.Tick tick)
        {
            //Debug.Log("tickIndex : " + tick.Index.ToString() + "...netTickIndex : " + netTickIndex);

            if (tick.Index <= netTickIndex)             //NOTE: 收到的重复帧，直接丢弃
                return;

            if (leftSimularTimes > 0 || (tick.Index > (netTickIndex + 1)))        //NOTE: 错帧，加到错帧列表中
            {
                InsertToList(laterTicks, tick);
                return;
            }

            netTickIndex++;
            //NOTE: 此处要处理 NetTick 与 LocalTick 相同的情况，此情况下 shotUpdated 为false
            //DoTick(tick);

            //while (laterTicks.Count > 0)
            //{
            //    if (laterTicks[0].Index > (netTickIndex + 1))
            //        break;

            //    netTickIndex++;
            //    DoTick(laterTicks[0]);
            //    laterTicks.RemoveAt(0);
            //}
            DoTick(tick);
        }

        //DoLocalTick 响应本地tick，融合修正 
        public void DoLocalTick(WarPb.Tick tick)
        {
            ////Debug.Log("DoLocalTick");
            //if (shotUpdated)        //NOTE: 如果状态更新了，重新计算
            //{
            //    shotUpdated = false;
            //    List<ItemInfo> curShot = new List<ItemInfo>();
            //    Snapshot(curShot);          //NOTE: 将当前状态保存各快照
            //    //NOTE: 此处从shot 开始快速演示到上一tick，并且根据传入tick的方向做修正
            //    QuickPlay();
            //}
            ////else
            //DoTick(tick);
            //InsertToList(localTicks, tick);
        }

        private GameObject GetPrefab(int id)
        {
            GameObject prefab;
            switch (id)
            {
                default:
                    prefab = (GameObject)Resources.Load("Prefabs/CompleteTank");
                    break;
            }
            return prefab;
        }

        //NOTE: 从shot 开始 快速播放到当前tick
        private void QuickPlay()
        {
            Revert(netShot);        //NOTE: 场景中物体回放到NetTick 最新帧
            foreach (WarPb.Tick tick in localTicks)
            {
                DoTick(tick);
            }
        }

        //NOTE: 快照，将当前场景的所有可变元素的Item信息记录下来[包括场景中可被破坏的装饰物]
        private void Snapshot(List<ItemInfo> shot)
        {
            shot.Clear();
            //foreach (ulong uid in itemMap.Keys)
            //{
            //    ItemInfo ii = itemMap[uid].GetInfo();
            //    ii.Uid = uid;
            //    shot.Add(ii);
            //}
        }

        private void Revert(List<ItemInfo> shot)
        {
            //foreach (ItemInfo ii in shot)
            //{
            //    Item item;
            //    if (itemMap.TryGetValue(ii.Uid, out item))
            //    {
            //        item.transform.position = ii.P;
            //        item.DV = ii.DV;
            //    }
            //}
        }

        private void DoTick(WarPb.Tick tick)
        {
            //Debug.Log("do tick index : " + tick.Index);
            if (tick.Actions.Count > 0)
            {
                foreach (var any in tick.Actions)
                {
                    if (any.Is(WarPb.Move.Descriptor))
                    {
                        var move = any.Unpack<WarPb.Move>();
                        var tm = GetTM(move.Uid);

                        var speed = move.Speed;
                        tm.MoveForward = (new Vector3(float.Parse(speed.X), 0f, float.Parse(speed.Z))).normalized;
                        continue;
                    }
                    if (any.Is(WarPb.Shoot.Descriptor))
                    {
                        Debug.Log("do shoot");
                        var shoot = any.Unpack<WarPb.Shoot>();
                        var tm = GetTM(shoot.Uid);
                        tm.FireInfo = shoot;
                        continue;
                    }
                }
            }
            //foreach (var tm in m_Tanks)
            //{
            //    tm.Do(0.06f);
            //}
            //Physics.defaultPhysicsScene.Simulate(0.06f);
            leftSimularTimes = 3;
            curTick = tick;
        }

        //Fixed 每次Fixed 调用一次，使用这个方法调用而不是使用Item中定义FixedUpdate 方法，因为这样容易控制顺序
        public void Fixed(float dt)
        {
            if (leftSimularTimes > 0)
            {
                string infoStr = "";
                infoStr += "__x:" + m_Tanks[0].m_Instance.transform.position.x + " y:" + m_Tanks[0].m_Instance.transform.position.y + " z:" + m_Tanks[0].m_Instance.transform.position.z + "|";
                foreach (var tm in m_Tanks)
                {
                    tm.Do(dt);
                }
                Debug.Log("" + curTick.Index + infoStr);
                Physics.defaultPhysicsScene.Simulate(dt);
                leftSimularTimes--;

                if ((leftSimularTimes == 0) && (laterTicks.Count > 0))
                {
                    if (laterTicks[0].Index == (netTickIndex + 1))
                    {
                        netTickIndex++;
                        DoTick(laterTicks[0]);
                        laterTicks.RemoveAt(0);
                    }
                }
            }
            //foreach (var tm in m_Tanks)
            //{
            //    tm.Do(dt);
            //}
        }

        //private void LateUpdate()
        //{

        //    //var pos = copyRole.transform.position;
        //    copyRole.transform.position = new Vector3(1, 0, 0);
        //}

        private Item GetItem(ulong uid)
        {
            //Item item;
            //return itemMap.TryGetValue(uid, out item) ? item : null;
            return null;
        }

        ////NOTE： 处理射击，会生成一个信息子弹Item
        //private void DoShoot(WarPb.Shoot shoot)
        //{
        //    Item rootItem = GetItem(shoot.Uid);
        //    var bullet = rootItem.NewBullet(shoot.Super);
        //    bullet.transform.parent = plane.transform;
        //}
             
        private void InsertToList(List<WarPb.Tick> list, WarPb.Tick tick)
        {

            if (list.FindIndex((c) => { return c.Index == tick.Index; }) != -1)     //NOTE: 冗余帧
                return;

            int idx = list.FindIndex((c) => { return c.Index > tick.Index; });
            if (idx != -1)
            {
                list.Insert(idx, tick);
            }
            else
                list.Add(tick);
        }
    }
}
