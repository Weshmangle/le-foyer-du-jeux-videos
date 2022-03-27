using Godot;
using System;

public class Card : Node
{
    [Signal] public delegate void Card_Clicked(Card card);
    
    protected bool played = false;

    public void SetVisual(Texture texture)
    {
        
    }

    private void ClickCard()
    {
        if(played)
        {
            GetNode<Button>("Control/IncreaseHealth").Visible = !GetNode<Button>("Control/IncreaseHealth").Visible;
            GetNode<Button>("Control/KillCard").Visible = !GetNode<Button>("Control/KillCard").Visible;
        }
        else
        {
            played = true;
            EmitSignal("Card_Clicked", this);
        }
    }

    private void MouseEnter()
    {
        if(!played) return;
        //GetNode<Button>("Control/IncreaseHealth").Visible = true;
        //GetNode<Button>("Control/KillCard").Visible = true;
    }

    private void MouseExited()
    {
        if(!played) return;
        //GetNode<Button>("Control/IncreaseHealth").Visible = false;
        //GetNode<Button>("Control/KillCard").Visible = false;
    }
}
