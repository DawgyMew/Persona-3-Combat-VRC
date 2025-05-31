
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using TMPro;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class startBattle : UdonSharpBehaviour
{
    public TextMeshPro num;
    public turnLogic TL;
    [UdonSynced, FieldChangeCallback(nameof(Shown))] private bool _shown = true;
    [UdonSynced, FieldChangeCallback(nameof(Count))] private int _count = 0;

    // basically deserialization
    private int Count
    {
        get => _count;
        set
        {
            _count = value;
            num.text = _count.ToString();
        }
    }

    private bool Shown
    {
        get => _shown;
        set
        {
            _shown = value;
            hideBox();
            TL.beforeBattle();
        }
    }

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        Count++;
        Shown = false;
        // SendCustomNetworkEvent(NetworkEventTarget.All, "hideBox");
        // TL.startBattle();
        RequestSerialization();
    }

    public void hideBox()
    {
        if (!Shown)
        {
            this.gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }
}
