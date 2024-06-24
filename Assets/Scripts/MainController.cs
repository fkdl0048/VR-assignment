using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public TextController textController;
    // Start is called before the first frame update
    void Start()
    {
        textController.StartText();
    }
    
    public void QuitApplication()
    {
        // 에디터 모드에서는 플레이 모드 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 애플리케이션 종료
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Foo()
    {
        Debug.Log("Foo");
    }
}
