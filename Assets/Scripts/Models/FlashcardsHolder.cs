using System;
using System.Collections.Generic;

[Serializable]
public class FlashcardsHolder
{
	public Dictionary<string, FlashcardInfo> flashcards = new();
}
