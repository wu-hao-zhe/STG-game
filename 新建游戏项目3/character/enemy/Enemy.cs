using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export] public Area2D HitArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HitArea.AreaEntered += (otherArea) =>	{
			GD.Print(1);
			QueueFree();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var p = GlobalPosition;
		p.Y+=5;
		GlobalPosition=p;
	}
}
