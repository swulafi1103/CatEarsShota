using System.Text;
using TMPro;
using UnityEngine;

public class MemmoryCheck : MonoBehaviour
{
    public UnityEngine.UI.Text m_text;

    private readonly UnityMemoryChecker m_unityMemoryChecker =
        new UnityMemoryChecker();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        m_unityMemoryChecker.Update();

        var sb = new StringBuilder();
        sb.AppendLine("<b>Unity</b>");
        sb.AppendLine($"    Used: {m_unityMemoryChecker.UsedText}");
        sb.AppendLine($"    Unused: {m_unityMemoryChecker.UnusedText}");
        sb.AppendLine($"    Total: {m_unityMemoryChecker.TotalText}");

        var text = sb.ToString();
        m_text.text = text;
    }
}