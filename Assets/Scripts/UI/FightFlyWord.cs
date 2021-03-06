using UnityEngine;

namespace UnityMMO
{
public class FightFlyWord : MonoBehaviour {
    Cocos.ActionRunner runner;
    TMPro.TextMeshPro label;

    private void Awake() 
    {
        runner = gameObject.AddComponent<Cocos.ActionRunner>();
        label = gameObject.GetComponent<TMPro.TextMeshPro>();
    }

    private void Update() 
    {
        //始终面向摄像机
        transform.rotation = Camera.main.transform.rotation;
    }

    public void StartFly()
    {
        var moveAction = Cocos.MoveBy.CreateAbs(0.7f, new Vector3(0, 1, 0));
        var fadeOutAction = Cocos.FadeOut.Create(0.3f, Cocos.ColorAttrCatcherTextMeshPro.Ins);
        var delayFadeoutAction = Cocos.Sequence.Create(Cocos.DelayTime.Create(0.4f), fadeOutAction);
        var spawnAction = Cocos.Spawn.Create(moveAction, delayFadeoutAction);
        var action = Cocos.Sequence.Create(spawnAction, Cocos.CallFunc.Create(()=>{
            Object.Destroy(gameObject, 0.1f);
        }));
        runner.PlayAction(action);
    }
    
    public void SetData(long num, long flag)
    {
        string showStr = "";
        string numStyle = "";
        if (flag == 0)
        {
            numStyle = "damage_mainrole_";
        }
        else if (flag == 1)
        {
            numStyle = "baoji_mainrole_";
            showStr = "<sprite name=\"baoji_mainrole\">";
        }
        else if (flag == 2)
        {
            numStyle = "baoji_other_";
        }
        string numStr = num.ToString();
        for (int i = 0; i < numStr.Length; i++)
        {
            showStr += "<sprite name=\""+numStyle+numStr[i]+"\">";
        }
        label.text = showStr;
    }

}

}