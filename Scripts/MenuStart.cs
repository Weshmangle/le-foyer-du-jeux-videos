using Godot;
using System;
using System.Collections.Generic;

public struct DeckInfos
{
    public string name;
    public string path;
    public int count;
}

public class MenuStart : Node2D
{
    private List<DeckPreview> decks = new List<DeckPreview>();
    public static readonly string PATH_DECKS = "res://decks/";

    #pragma warning disable 649
    [Export] public PackedScene DeckPreviewScene;
    #pragma warning restore 649

    public static MenuStart Instance;
    public DeckPreview deckSelect = null;
    public DeckPreview [] deckPlayerSelected = new DeckPreview [2];

    protected List<DeckInfos> getDecksInfos()
    {
        string[] files = FileUtils.ListDirectory(PATH_DECKS);
        
        List<DeckInfos> decksInfos = new List<DeckInfos>();

        foreach (var file in files)
        {
            var split = file.ToLower().Split("_");

            if(split[0] == "deck")
            {
                var deckInfos = new DeckInfos();
                deckInfos.name = split[1];
                deckInfos.path = PATH_DECKS + "" + file;
                deckInfos.count = FileUtils.ListDirectory(deckInfos.path).Length;
                
                decksInfos.Add(deckInfos);
            }
        }

        return decksInfos;
    }

    protected void addDeckToView(List<DeckInfos> decksInfos)
    {
        Node scrollContainer = FindNode("VScrollBar_Card");

        foreach (var deckInfos in decksInfos)
        {
            DeckPreview deck = DeckPreviewScene.Instance<DeckPreview>();
            deck.SetPathCard(deckInfos);
            scrollContainer.AddChild(deck);
            decks.Add(deck);
        }
    }

    protected void SetDeckSelected(int index)
    {
        if(deckSelect != null)
        {
            deckPlayerSelected[index] = deckSelect;
            GetNode<TextureRect>($"MainContainer/CardsContainer/PreviewDeckSelected/Deck{index+1}Preview").Texture = deckSelect.textureBackCard;
        }
    }

    public override void _Ready()
    {
        Instance = this;

        var decksInfos = getDecksInfos();
        
        addDeckToView(decksInfos);
    }

    public void DeckPlayer1Selected()
    {
        SetDeckSelected(0);
    }
    
    public void DeckPlayer2Selected()
    {
        SetDeckSelected(1);
    }

    public void StartGame()
    {
        //PackedScene a = ResourceLoader.Load<PackedScene>("res://Scene/Game.tscn").Instance();
        //var aa = (PackedScene)ResourceLoader.Load("res://levels/level2.tscn").instance();
        //GetTree().Root.AddChild(a);
        
        GetTree().ChangeScene("res://Scene/Game.tscn");
    }

    public void RefreshDeckSelected(DeckPreview deckSelected)
    {
        MenuStart.Instance.deckSelect = deckSelected;

        foreach (var deck in decks)
        {
            deck.GetNode<ColorRect>("Control/ColorSelect").Visible = false;
        }
        
        deckSelected.GetNode<ColorRect>("Control/ColorSelect").Visible = true;
    }
}
