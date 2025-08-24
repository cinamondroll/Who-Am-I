using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{
    public List<DialogNode> nodes;  // Daftar node dalam dialog

    [System.Serializable]
    public class DialogNode
    {
        public string speakerName;  // Nama pembicara
        public string text;         // Teks dialog

        public Sprite image;
        public List<Choice> choices = new List<Choice>();  // Pilihan dalam dialog

        [System.Serializable]
        public class Choice
        {
            public string choiceText;  // Teks pilihan
            public int nextNodeIndex; // Indeks node berikutnya yang akan ditampilkan jika pilihan ini dipilih
        }
    }
}
