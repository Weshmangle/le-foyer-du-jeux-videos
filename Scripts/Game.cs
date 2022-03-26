using Godot;
using System;

public class Game : Node
{
    private Deck deckPlayer1;
    private Deck deckPlayer2;

    private BoardPlayer boardPlayer1;
    private BoardPlayer boardPlayer2;

    protected BoardPlayer currentPlayer;
    protected Button buttonEndTurn;
    protected int turn = 0;

    public override void _Ready()
    {
        boardPlayer1 = GetNode<BoardPlayer>("VBoxContainer/CenterContainer/Board1");
        boardPlayer2 = GetNode<BoardPlayer>("VBoxContainer/CenterContainer/Board2");
        buttonEndTurn = GetNode<Button>("Control/ButtonEndTurn");
        LoadDeck(MenuStart.Instance.deckPlayerSelected);
        boardPlayer1.SetDeck(deckPlayer1);
        boardPlayer2.SetDeck(deckPlayer2);
        StartGame();
    }
    
    public void ButtonEndTurnPressed()
    {
        FinishTurn();
    }
    
    public void LoadDeck(DeckPreview [] deckPreviews)
    {
        deckPlayer1 = ResourceLoader.Load<PackedScene>("res://Scene/Deck.tscn").Instance<Deck>();
        deckPlayer2 = ResourceLoader.Load<PackedScene>("res://Scene/Deck.tscn").Instance<Deck>();
        deckPlayer1.LoadDeck(deckPreviews[0].deckInfo);
        deckPlayer2.LoadDeck(deckPreviews[1].deckInfo);
    }

    public void StartGame()
    {
        deckPlayer1.ShuffledDeck();
        deckPlayer2.ShuffledDeck();
        SelectPlayer1();
    }

    public void FinishTurn()
    {
       if(currentPlayer == boardPlayer1)
       {
           SelectPlayer2();
       }
       else
       {
           SelectPlayer1();
       }
    }

    protected void SelectPlayer1()
    {
        turn++;
        GetNode<Label>("VBoxContainer/TurnPlayer").Text = "Turn Player 1";
        currentPlayer = boardPlayer1;
        boardPlayer1.Visible = true;
        boardPlayer2.Visible = false;
    }
    protected void SelectPlayer2()
    {
        GetNode<Label>("VBoxContainer/TurnPlayer").Text = "Turn Player 2";
        currentPlayer = boardPlayer2;
        boardPlayer1.Visible = false;
        boardPlayer2.Visible = true;
    }

    protected void PickCardStart()
    {
        for (var i = 0; i < 3; i++)
        {
            boardPlayer1.PickCard();
        }
    }
}
