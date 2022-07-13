using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _character = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI _textOut;

    private int _indexCharacter = 0;
    private int _indexText = 0;

    private DialogCharacter _dialogCharacter;
    
    private void Start()
    {
        WriteDialog();
        _indexCharacter++;
    }

    private void WriteDialog()
    {
        _dialogCharacter = _character[_indexCharacter].GetComponent<DialogCharacter>();
                            
        if (_indexText >= _dialogCharacter.GetReadCount())
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            string text = _dialogCharacter.GetStringIndex(_indexText);
                            
            TextDialog(text);
        }
    }

    private void TextDialog(string text)
    {
        _textOut.text = text;
    }

    private void NextIndex()
    {
        for (int i = _indexCharacter; i < _character.Count; i++)
        {
            WriteDialog();

            _indexCharacter++;
            return;
        }

        if (_indexCharacter >= _character.Count)
        {
            _indexCharacter = 0;
            _indexText++;
            WriteDialog();
        }
    }

    private void Update()
    {
        Switch();
    }

    private void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NextIndex();
            _indexCharacter++;
        }
        
    }
}
