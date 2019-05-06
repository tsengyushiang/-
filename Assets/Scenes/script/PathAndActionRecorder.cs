using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  //StreamWrite會用到
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PathAndActionRecorder : MonoBehaviour {

    public float RecordRate = 1.0f;
    public float ReplayTime = 10f;
    public GameObject RecordObj;
    public GameObject ReplayObj;
    public LineRenderer lr;
    private int replayIndex = 0;

    public static int DayCount=0;
    public static string RecordPath;
    public static int playTime;
    public static string uploadUrl;

    public List<Vector3> Path=new List<Vector3>();
    public static Text DebugText;
    public MonoBehaviour Recorder;
    void Awake() {
        DebugText = GameObject.Find("Canvas/Text").GetComponent<Text>();

        Path.Clear();
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.json");
        FileInfo[] configFile = directoryInfo.GetFiles("config.txt");

        StreamReader strReader = configFile[0].OpenText();
        playTime = int.Parse(strReader.ReadLine());
        uploadUrl = strReader.ReadLine().Replace('\\', '/');

        RecordPath = Application.streamingAssetsPath;
        //RecordPath = strReader.ReadLine().Replace('\\','/');
        //DebugText.text=(RecordPath);


        DayCount = allFiles.Length + 1;
        
    }

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

        if (replayIndex >= (Records.Count - 1)) return;

        ReplayObj.GetComponent<setSprite>().setAction(
            Records[replayIndex].pos, Records[replayIndex].currentAnimation, Records[replayIndex].index);

        if (Path.ToArray().Length < Records.Count - 1)
        {
            Path.Add(Records[replayIndex].pos);
            lr.positionCount = Path.ToArray().Length;
            lr.SetPositions(Path.ToArray());
        }       

        replayIndex++;

    }

    // Start is called before the first frame update
    public void StartRecord()
    {
        Records.Clear();
        InvokeRepeating("Record", 0f, RecordRate);
    }

    public void StartReplay() {        
        InvokeRepeating("Replay", 0f, (float)ReplayTime / Records.Count);
    }

    public void Stop() {
        CancelInvoke();
    }
   
    public void save()
    {      
        string saveString = "";
        foreach (status s in Records) {
            saveString += JsonUtility.ToJson(s) + "\n";
        }

        System.IO.File.WriteAllText(Application.streamingAssetsPath +"/"+ (DayCount).ToString() + ".json",saveString);
    }
    void Load() {

        object[] files = Resources.LoadAll("records");

        string fileString = files[files.Length - 1].ToString();

        string[] sentense = fileString.Split('\n');
        
        foreach (string s in sentense)
        {
            if(s=="")continue;
            Records.Add(JsonUtility.FromJson<status>(s));
        }

    }

    public  static void debug(string s) {
        //DebugText.text += (s) + "\n";
        //Debug.Log(s);
    }


}
