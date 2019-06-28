using System;
using System.Collections.Generic;
using UnityEngine;


namespace Moba.Views
{

    public class ItemInfo
    {
        public DecimalVector3 DV { set; get; }
        public Vector3 P { set; get; }
        //public float Speed { set; get; }
        public ulong Uid { set; get; }
    }

    public class Item : MonoBehaviour
    {
        //public GameObject GO { set; get; }      //NOTE: gameobject
        public DecimalVector3 DV { set; get; }  //NOTE: 移动方向，各个方向上的速度缩放
        //public float SPEED { set; get; }        //NOTE: 移动速度
        [SerializeField]
        private float speed = 0.0f;
        [SerializeField]
        private GameObject bulletPrefab;        //NOTE: 对应的子弹预设
        [SerializeField]
        private GameObject bigBulletPrefab;     //NOTE: 大招对应的子弹预设
        //private GameObject prefab;

        public enum STATE_TAG
        {
            IDLE = 1,
            WALK = 2,
            ATTACK1 = 4,
            ATTACK2 = 5,
            GETHIT = 8,
            DEATH = 16,
            MAX = 32
        }

        private Animator amt = null;

        //Instantiate 实例化一个对象
        //public void Instantiate()
        //{

        //    GO = (GameObject)Instantiate(prefab);
        //    DV = new DecimalVector3();
        //}
        private CharacterController cc;

        private void Start()
        {
            cc = gameObject.GetComponent<CharacterController>();
            //cc.transform.position = transform.position;
            //cc.transform.localPosition = transform.localPosition;
        }

        //GetInfo 获得对象的状态信息，用于快照 恢复
        public ItemInfo GetInfo()
        {
            ItemInfo ii = new ItemInfo();
            ii.DV = new DecimalVector3(DV);
            ii.P = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //ii.Speed = speed;
            return ii;
        }

        //private void LateUpdate()
        //{
        //    if (!gameObject)
        //        return;

        //    float fix = Time.deltaTime * speed;
        //    if (!DV.IsZero())
        //    {
        //        //var curPos = gameObject.transform.localPosition;
        //        Vector3 moveDir = transform.forward * fix;
        //        moveDir.y = 0;
        //        cc.Move(moveDir);


        //        //cc.SimpleMove(new Vector3(-(float)DV.x, 0, -(float)DV.z));
        //        //var curPos = gameObject.transform.localPosition;
        //        //gameObject.transform.localPosition = new Vector3();
        //        Transition(STATE_TAG.WALK);
        //    }
        //    else
        //        Transition(STATE_TAG.IDLE);
        //}

        public void Do(float dt)
        {
            if (!gameObject)
                return;

            float fix = dt * speed;
            if (!DV.IsZero())
            {
                //var curPos = gameObject.transform.localPosition;
                Vector3 moveDir = transform.forward * fix;
                moveDir.y = 0;
                cc.Move(moveDir);

                Transition(STATE_TAG.WALK);
            }
            else
                Transition(STATE_TAG.IDLE);
        }


        public void DoMove(WarPb.Move move)
        {
            DV = new DecimalVector3(move.Speed);
        }

        //NOTE: 生成子弹，big 用于区别是否为大招
        public Item NewBullet(bool big)
        {
            var obj = GameObject.Instantiate(big ? bigBulletPrefab : bulletPrefab);
            Item bullet = obj.GetComponent<Item>();
            bullet.transform.position = transform.position + DV.ToVector3();
            return bullet;
        }

        //public void DoShoot(WarPb.Shoot shoot)
        //{

        //}


        public void Transition(STATE_TAG dst)
        {
            if (amt == null)
                amt = gameObject.GetComponent<Animator>();

            int dstTag = (int)dst;
            var info = amt.GetCurrentAnimatorStateInfo(0);
            if (info.IsTag(dstTag.ToString()))
                return;

            int curTag = 0;
            foreach (int tag in Enum.GetValues(typeof(STATE_TAG)))
            {
                if (!info.IsTag(tag.ToString()))
                    continue;

                switch (tag)
                {
                    case (int)STATE_TAG.IDLE:
                    case (int)STATE_TAG.WALK:
                        curTag = tag;
                        break;     //NOTE: 只有移动或待机状态响应其它动作
                    default:
                        return;
                }
                break;
            }
            var num = curTag ^ dstTag;
            if (dstTag < curTag)
                num ^= (int)STATE_TAG.MAX;

            amt.SetInteger("Action", num);
        }
    }

    public class ItemFactory
    {
        private static Dictionary<int, GameObject> prefabDic = new Dictionary<int, GameObject>();
        public static Item InstantiateByPBRole(MasterPb.Role role, Vector3 point)
        {

            GameObject prefab;
            if (!prefabDic.ContainsKey(role.ModelId))
            {
                string preName;
                switch (role.ModelId)
                {
                    case 0:
                        preName = "Prefabs/CompleteTank";
                        break;
                    //case 1:
                    //    preName = "Prefabs/GruntHP";
                    //    break;
                    //case 2:
                    //    break;
                    //case 3:
                    //    break;
                    default:
                        preName = "Prefabs/CompleteTank";
                        break;
                }
                prefab = (GameObject)Resources.Load(preName);
                prefabDic[role.ModelId] = prefab;
            }
            else
                prefab = prefabDic[role.ModelId];

            GameObject go = GameObject.Instantiate(prefab, point, prefab.transform.rotation);
            Item item = go.GetComponent<Item>();
            return item;
        }
    }
}
