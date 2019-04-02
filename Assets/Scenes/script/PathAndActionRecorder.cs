using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  //StreamWrite會用到
using UnityEditor;
using UnityEngine.UI;

public class PathAndActionRecorder : MonoBehaviour {

    public float RecordRate = 1.0f;
    public float ReplayTime = 10f;
    public GameObject RecordObj;
    public GameObject ReplayObj;
    private int replayIndex = 0;
    public static int DayCount=0;
    public List<Vector3> Path=new List<Vector3>();
    public Text DebugText;
    void Awake() {

        Path.Clear();
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.json");
        DayCount = allFiles.Length + 1;

        Debug.Log(Application.streamingAssetsPath);
        if (allFiles.Length > 0) {
            UploadFile(
                  Application.streamingAssetsPath + "/" +
                  allFiles.Length.ToString() + ".gif",
                  "http://140.118.127.121/game/");
        }        

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

        if (replayIndex >= (Records.Count - 1)) replayIndex = 0;

        ReplayObj.GetComponent<setSprite>().setAction(
            Records[replayIndex].pos, Records[replayIndex].currentAnimation, Records[replayIndex].index);

        if (Path.ToArray().Length < Records.Count-1) {
            Path.Add(Records[replayIndex].pos + new Vector3(-0.73f, -0.59f, 0));
            GetComponent<LineRenderer>().positionCount = Path.ToArray().Length;
            GetComponent<LineRenderer>().SetPositions(Path.ToArray());
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

    #region UpLoadToWebServer
    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        Debug.Log(localFileName);
        WWW localFile = new WWW("file:///" + localFileName);
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        postForm.AddBinaryData(
            "theFile",
            localFile.bytes,
            (DayCount-1).ToString() + ".gif",
            "image/gif");

        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }
    void UploadFile(string localFileName, string uploadURL)
    {
        StartCoroutine(UploadFileCo(localFileName, uploadURL));
    }
    #endregion



}
