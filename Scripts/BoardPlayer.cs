using Godot;
using System;
using System.Collections.Generic;

public class BoardPlayer : CenterContainer
{
    public static int COUNT_HAND = 7;
    public static int COUNT_PLAYED = 6;
    private List<Card> trash = new List<Card>();
    private List<Card> stack = new List<Card>();
    private Container played;
    private Container hand;
    private Deck deck;

    public override void _Ready()
    {
        hand = GetNode<Container>("VBoxContainer/CenterContainer/Hand");
        played = GetNode<Container>("VBoxContainer/front/Played");
    }

    public void SetDeck(Deck deck)
    {
        this.deck = deck;
    }

    public void StackClicked()
    {
        PickCard();
    }

    public void CardCliked(Card card)
    {
        hand.RemoveChild(card);
        played.AddChild(card);
    }

    public void PickCard()
    {
        var card = deck.PickCard();
        
        if(hand.GetChildren().Count >= COUNT_HAND)
        {
            trash.Add(card);
        }
        else
        {
            hand.AddChild(card);
            card.Connect("Card_Clicked", this, nameof(CardCliked));
        }
    }
}
