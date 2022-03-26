using Godot;
using System;
using System.Collections.Generic;

public class Deck : Control
{
    #pragma warning disable 649
    [Export] public PackedScene CardScene;
    #pragma warning restore 649
    
    protected DeckInfos deckInfos;
    protected List<Card> cards = new List<Card>();

    public override void _Ready()
    {
        CardScene = ResourceLoader.Load<PackedScene>("res://Scene/Card.tscn");
    }
    
    protected void LoadCards()
    {
        string[] filesCards = FileUtils.ListDirectory(deckInfos.path, "import");

        foreach (var fileCard in filesCards)
        {
            var card = CardScene.Instance<Card>();
            card.SetVisual(GD.Load<Texture>(deckInfos.path + "/" + fileCard));
            cards.Add(card);
        }
    }

    public void LoadDeck(DeckInfos deckInfos)
    {
        this.deckInfos = deckInfos;
        LoadCards();
    }

    public List<Card> GetCards()
    {
        return new List<Card>(cards);
    }

    public void ShuffledDeck()
    {
        int n = cards.Count;  
        while (n > 1)
        {  
            n--;  
            int k = new Random().Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public Card PickCard()
    {
        if(cards.Count > 0)
        {
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        else
        {
            return null;
        }
    }
}
