using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public PackedScene BulletPrefab;
	// [Export] public Area2D HitArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 InputVec = new(
			Input.GetAxis("ui_left","ui_right"),
			0
		);
		GlobalPosition += InputVec * 10;
		MoveAndSlide();
	}
	[Export] public double Interval=0.15;
	double _accumulator=0;
	public override void _PhysicsProcess(double delta){
		_accumulator+=delta;
		if(_accumulator<Interval)return;
		_accumulator-=Interval;
		Shoot();
	}
	void Shoot(){
		var b=BulletPrefab.Instantiate() as Node2D;
		GetParent().AddChild(b);
		b.GlobalPosition=GlobalPosition;

	}
}
