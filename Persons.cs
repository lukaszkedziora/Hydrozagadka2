using System;

namespace Hydrozagadka2 {

    public class Persons : Characters , IDialogues {

        public bool isUsed;
        public string dialogues;

        public string GetSentence(){
            throw new NotImplementedException();
        }

        public string GetAnswer(){
            throw new NotImplementedException();
        }

    }
}