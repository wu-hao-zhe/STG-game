using Godot;
using System;

public partial class Game : Node2D
{
	// 全局计数/编号。加 [Export] 后可以在编辑器检查器里直接看到并修改（会存进 Game.tscn）。
	[Export] public int num = 0;

	// 静态入口：让 Boss、Enemy 等子场景不用关心节点路径就能拿到 Game。
	public static Game Instance { get; private set; }

	[Export] public PackedScene EnemyPrefab,BossPrefab;
	[Export] public Node2D WorldRoot;
	[Export] public Timer SpawnTime;
	[Export] public Label _score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		for(int i = 1,t=-600; i <= 50; i++,t-=300)
		{
		var pos=new Vector2(-394, t);
		var e=BossPrefab.Instantiate() as Boss;
		WorldRoot.AddChild(e);
		e.GlobalPosition=pos;
		e.a=Math.Min(i*7,70);
		e.f=i==50;
		}
		SpawnTime.Timeout += () =>
		{
			SpawnEnemy();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_score.Text=num+"/50";
		if (Input.IsActionPressed("ui_cancel"))
		{
			// GetTree().Paused=!GetTree().Paused;
			GetTree().Quit();
		}
	}

	public override void _ExitTree()
	{
		// 场景被卸载时清掉静态引用，避免下次进入游戏拿到已释放的 Game。
		if (Instance == this)
		{
			Instance = null;
		}
	}

	void SpawnEnemy()
	{
		if(GD.RandRange(0,3)==3)return;
		var pos=new Vector2(GD.RandRange(-200,GetWindow().Size.X-800),-1000);
		var e=EnemyPrefab.Instantiate() as Enemy;
		WorldRoot.AddChild(e);
		e.GlobalPosition=pos;
	}
}
