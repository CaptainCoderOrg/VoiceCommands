using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class CommandDetector : MonoBehaviour
{
    private DictationRecognizer _dictationRecognizer;

    private SpeechSystemStatus _currentStatus;

    [SerializeField]
    private string[] _commands;
    
    [field: SerializeField]
    public UnityEvent<string> OnStatusChange { get; private set; }

    [field: SerializeField]
    public UnityEvent<string> OnCommand { get; private set; }
    
    [field: SerializeField]
    public UnityEvent<string> OnHypothesisUpdate { get; private set; }

    

    // Start is called before the first frame update
    void Start()
    {
        _dictationRecognizer = new DictationRecognizer(ConfidenceLevel.Low);
        _currentStatus = _dictationRecognizer.Status;
        OnStatusChange.Invoke(_currentStatus.ToString());
        _dictationRecognizer.DictationHypothesis += OnHypothesisChange;
        OnStatusChange.AddListener(StatusChanged);

        // m_DictationRecognizer.DictationResult += (text, confidence) =>
        // {
        //     Debug.LogFormat("Dictation result: {0}", text);
        //     m_Recognitions.text = text + "\n";
        // };

        // m_DictationRecognizer.DictationHypothesis += (text) =>
        // {
        //     Debug.LogFormat("Dictation hypothesis: {0}", text);
        //     m_Hypotheses.text = text;
        // };

        // m_DictationRecognizer.DictationComplete += (completionCause) =>
        // {
        //     if (completionCause != DictationCompletionCause.Complete)
        //         Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        // };

        // m_DictationRecognizer.DictationError += (error, hresult) =>
        // {
        //     Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        // };

        // m_DictationRecognizer.Start();
        _dictationRecognizer.Start();
    }

    private void OnHypothesisChange(string text)
    {
        OnHypothesisUpdate.Invoke(text);
        // Debug.LogFormat("Dictation hypothesis: {0}", text);
        string[] tokens = text.ToLowerInvariant().Split(null);
        foreach(string token in tokens)
        {
            if (_commands.Contains(token))
            {
                Debug.Log($"Command Detected: {token}");
                OnCommand.Invoke(token);
                _dictationRecognizer.Stop();
                return;
            }
        }
    }

    private void StatusChanged(string newStatus)
    {
        if (newStatus == SpeechSystemStatus.Stopped.ToString())
        {
            _dictationRecognizer.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {        
        SpeechSystemStatus status = _dictationRecognizer.Status;
        if (_currentStatus != status)
        {
            _currentStatus = status;
            OnStatusChange.Invoke(_currentStatus.ToString());
        }
        
    }
}
