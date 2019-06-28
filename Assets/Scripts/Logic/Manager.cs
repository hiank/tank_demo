using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Moba.Events;

namespace Moba.Logic
{
    public class Manager : MonoBehaviour
    {
        //private Meet meet;
        //private Dictionary<string, Item> hashMap = new Dictionary<string, Item>();

        private void Awake()
        {

            EventNet.Instance.Get(WarPb.War_Match.Descriptor.FullName).AddListener(new UnityAction<Google.Protobuf.WellKnownTypes.Any>(handleNetMatch));
            //EventLogic.Instance.AddListener(new UnityAction<EventLogicTickList>(handleLogicTick));     //NOTE: 监听逻辑层的指令

            //meet = new Meet();
            //var activeScene = SceneManager.GetActiveScene();

            //WarPb.War_Match match = new WarPb.War_Match();
            //ulong uid = 1;
            //for (int i = 0; i < 2; i++)
            //{
            //    WarPb.Team team = new WarPb.Team();
            //    for (int n = 0; n < 3; n++)
            //    {
            //        MasterPb.Role role = new MasterPb.Role();
            //        role.ModelId = n;
            //        role.Uid = uid++;
            //        team.Roles.Add(role);
            //    }
            //    match.League.Add(team);
            //}
            //meet.DoMatch(match, activeScene);
            //Physics.autoSimulation = false;
        }

        // Start is called before the first frame update
        private void Start()
        {

            //int t = (1 << 13) ^ (1 << 9) ^ (1 << 5);
            //var item = meet.Hero;
            //item.Instantiate();
            //External.SetTag(item.GO, tag.ToString());
            //hashMap[tag.ToString()] = item;
            //string _tag = t.ToString();
            //External.AddTag(_tag);
            //item.gameObject.tag = _tag;

            //EventFollow.Instance.Invoke(item.transform);

            WarPb.War_Match match = new WarPb.War_Match();
            ulong uid = 1;
            for (int i = 0; i < 2; i++)
            {
                WarPb.Team team = new WarPb.Team();
                for (int n = 0; n < 3; n++)
                {
                    MasterPb.Role role = new MasterPb.Role();
                    role.ModelId = n;
                    role.Uid = uid++;
                    team.Roles.Add(role);
                }
                match.League.Add(team);
            }

            var any = Google.Protobuf.WellKnownTypes.Any.Pack(match);

            EventNet.Instance.Get(Google.Protobuf.WellKnownTypes.Any.GetTypeName(any.TypeUrl)).Invoke(any);

        }


        //private void OnGUI()
        //{
        //    if (GUI.Button(new Rect(0, 0, 100, 30), "行走"))
        //    {
        //        string tag = "" + ((1 << 13) ^ (1 << 9) ^ (1 << 5));
        //        hashMap[tag].Transition(Item.STATE_TAG.WALK);
        //    }
        //    if (GUI.Button(new Rect(110, 0, 100, 30), "攻击"))
        //    {
        //        hashMap[tag].Transition(Item.STATE_TAG.ATTACK1);
        //    }
        //    if (GUI.Button(new Rect(220, 0, 100, 30), "攻击2"))
        //    {
        //        hashMap[tag].Transition(Item.STATE_TAG.ATTACK2);
        //    }
        //}

        //// Update is called once per frame
        //void Update()
        //{
        //    //float fix = Time.deltaTime * speed;

        //    foreach (string key in hashMap.Keys)
        //    {
        //        Item item = hashMap[key];
        //        //updateItem(item, fix);
        //        item.Do(Time.deltaTime);
        //    }
        //}

        //private void updateItem(Item item, float dt)
        //{

        //    if (!item.DV.IsZero())
        //    {
        //        var curPos = item.GO.transform.localPosition;
        //        item.GO.GetComponent<CharacterController>().Move(new Vector3(-dt * (float)item.DV.x, 0, -dt * (float)item.DV.z));
        //        transition(2);
        //    }
        //    else
        //        transition(1);
        //}

        //// handleLogicTick 处理逻辑层传过来的演示数据，更新目标状态
        //private void handleLogicTick(EventLogicTickList action)
        //{
        //    foreach (EventLogicTick tick in action)
        //    {

        //        Debug.Log("handle logic tick");
        //        //string tag = "" + tick.tag;
        //        //var item = hashMap[tag];
        //        //if (!item.GO.CompareTag(tag))
        //        //    continue;


        //        //item.DV = tick.v3;
        //        //if (tick.v3.IsZero())
        //        //    continue;

        //        //float radian = Mathf.Atan2(decimal.ToSingle(item.DV.z), decimal.ToSingle(item.DV.x));
        //        //float angle = radian / Mathf.PI * 180;

        //        //item.GO.transform.rotation = Quaternion.Euler(0, -90.0f - angle, 0);
        //    }
        //}

        //NOTE: 处理匹配消息完成
        private void handleNetMatch(Google.Protobuf.WellKnownTypes.Any any)
        {
            WarPb.War_Match match = any.Unpack<WarPb.War_Match>();
            GameObject prefab;
            switch (match.MapId)
            {
                default:
                    prefab = (GameObject)Resources.Load("Prefabs/CompleteLevelArt");
                    break;
            }
            GameObject plane = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(plane, gameObject.scene);

            //plane.GetComponent<Views.PlaneManager>().DoMatch(match);
            var pm = plane.GetComponent<PlaneManager>();
            pm.DoMatch(match);
            Driver d = gameObject.AddComponent<Driver>();           //NOTE: 添加Driver 组件，处理数据
            d.M = pm.m_Meet;
            EventFollow.Instance.Invoke(pm.Look);
        }
    }
}
