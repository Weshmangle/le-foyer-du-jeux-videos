using Godot;
using System;
using System.Collections.Generic;

public class DeckPreview : HBoxContainer
{
    public static readonly string nameBackCard = "card_back.png";

    public Texture textureBackCard;
    
    public DeckInfos deckInfo;

    protected void addLabelNotFoundBackCard()
    {
        var label = new Label();
        label.Text = "Back Card Not Found";
        label.AddColorOverride("font_color", new Color(.5f,.5f,.5f));
        var size = GetNode<Button>("Control/DeckBackCard").RectSize;
        size.y = size.y * .9f;
        label.SetSize(size);
        label.Align = Label.AlignEnum.Center;
        label.Valign = Label.VAlign.Bottom;
        GetNode<Button>("Control/DeckBackCard").AddChild(label);
    }
    
    public void SetPathCard(DeckInfos deckInfos)
    {
        deckInfo = deckInfos;
        
        var pathBackCard = deckInfos.path + "/" + nameBackCard;

        if(new File().FileExists(pathBackCard))
        {
            GetNode<Button>("Control/DeckBackCard").Icon = GD.Load<Texture>(pathBackCard);
            textureBackCard = GetNode<Button>("Control/DeckBackCard").Icon;
        }
        else
        {
            addLabelNotFoundBackCard();
        }
        
        GetNode<Label>("Control/DeckBackCard/DeckName").Text = deckInfos.name;
        GetNode<Label>("Control/DeckBackCard/DeckCount").Text = deckInfos.count.ToString();
    }

    public void OnMouseEntered()
    {
        Modulate = new Color(1,1,1,.5f);
    }
    public void OnMouseExited()
    {
        Modulate = new Color(1,1,1,1f);
    }

    public void CardClicked()
    {
        MenuStart.Instance.RefreshDeckSelected(this);
    }
}
