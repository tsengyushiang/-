using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  //StreamWrite會用到

public class PathAndActionRecorder : MonoBehaviour {

    public float RecordRate = 1.0f;
    public float ReplayRate = 1.0f;
    public GameObject RecordObj;
    public GameObject ReplayObj;

    [System.Serializable]
    public class status
    {
        public status(Vector3 p, string animat, int i) {
            this.pos = p;
            this.currentAnimation = animat;
            this.index = i;
        }
        public Vector3 pos;
        public string currentAnimation;
        public int index;
    };

    private List<status> Records = new List<status>();

    void Record()
    {
        Records.Add(new status(RecordObj.transform.position,
            RecordObj.GetComponent<animationNormal>().CurrentAnimationName,
            (int)RecordObj.GetComponent<animationNormal>().currentMotion
            ));
    }

    void Replay() {

        ReplayObj.GetComponent<setSprite>().setAction(
            Records[0].pos, Records[0].currentAnimation, Records[0].index);

        Records.RemoveAt(0);
        if (Records.ToArray().Length == 0)
            CancelInvoke();
    }

    // Start is called before the first frame update
    public void StartRecord()
    {
        Records.Clear();
        InvokeRepeating("Record", 0f, RecordRate);
    }

    public void StartReplay() {
        InvokeRepeating("Replay", 0f, ReplayRate);
    }

    public void Stop() {
        if (Records.ToArray().Length != 0)
        {
            save(Records);
        }
        CancelInvoke();
    }
    
    void save(List<status> data)
    {
        object[] files = Resources.LoadAll("records");
        // Debug.Log(JsonUtility.FromJson<status>(files[files.Length-1].ToString()).pos);

        string saveString = "";
        foreach (status s in data) {
            saveString += JsonUtility.ToJson(s)+"\n";
        }

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter("Assets/Resources/records/"+ (files.Length + 1).ToString() + ".json");
        file.Write(saveString);
        file.Close();
    }

}
