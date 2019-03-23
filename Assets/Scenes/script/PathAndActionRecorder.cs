using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  //StreamWrite會用到

public class PathAndActionRecorder : MonoBehaviour {

    public float RecordRate=1.0f;

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

    public ArrayList Records=new ArrayList();

    void TimerCountDown()
    {
        Records.Add(new status(transform.position,
            GetComponent<animationNormal>().CurrentAnimationName,
            (int)GetComponent<animationNormal>().currentMotion
            ));
    }
    // Start is called before the first frame update
    public void OnEnable()
    {
        InvokeRepeating("TimerCountDown", 0f, RecordRate);
    }

    void save(string fileName, ArrayList data)
    {
        string saveString = "";
        foreach (status s in data) {
            saveString += JsonUtility.ToJson(s)+"\n";
        }

        Debug.Log("save : "+saveString);

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter("Assets/Resources/records/"+fileName+".json");
        file.Write(saveString);
        file.Close();
    }

    public void OnDisable() {

        
        object[] files = Resources.LoadAll("records");
        save((files.Length + 1).ToString(), Records);

        // Debug.Log(JsonUtility.FromJson<status>(files[files.Length-1].ToString()).pos);

        CancelInvoke();
    }

}
