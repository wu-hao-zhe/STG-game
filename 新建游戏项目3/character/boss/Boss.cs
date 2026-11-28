using Godot;
using System;

public partial class Boss : CharacterBody2D
{
	// 子弹所在碰撞层（见 Bullet.tscn 的 collision_layer = 8）
	private const uint BulletLayer = 8;

	[Export] public Area2D HitArea;
	[Export] public PackedScene UIPrefab;
	[Export] public Node2D WorldRoot;
	public int a;
	public bool f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HitArea.AreaEntered += (otherArea) =>	{
			a--;

			// // 只有被子弹命中才算一次；Boss 之间互相重叠也会触发 area_entered，要过滤掉。
			// if (otherArea != null && (otherArea.CollisionLayer & BulletLayer) != 0)
			// {
			// 	AddToGameNum(1);
			// }
		};
	}

	// 在 Boss 子场景里修改 Game 的 num：通过 Game.Instance 静态入口拿到 Game 节点，
	// 然后像改普通字段一样直接 num += / num = 即可。
	void AddToGameNum(int delta)
	{
		var game = GetGame();
		if (game == null)
		{
			GD.PushWarning("Boss: 找不到 Game 节点, num 未修改");
			return;
		}

		game.num += delta;
		GD.Print($"[Boss] Game.num = {game.num}");
	}

	// 优先用静态入口；万一静态引用还没建立（比如在 Boss._Ready 里立刻调用），
	// 再退回父节点链——Boss 都是挂在 Game（WorldRoot = "."）下面的。
	Game GetGame()
	{
		return GetTree().CurrentScene as Game;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var p = GlobalPosition;
		p.Y+=3;
		GlobalPosition=p;
		MoveAndSlide();
		if (a < 0)
		{
			AddToGameNum(1);
			if (f)
			{
				GD.Print(10);
				var pos=new Vector2(0,0);
				var e=UIPrefab.Instantiate() as Node2D;
				GetTree().Root.AddChild(e);
				e.GlobalPosition=pos;
				GetTree().Paused=true;
			}
			QueueFree();
		}
		// int t=GetSlideCollisionCount();
		// if (t>0)
		// {
		// 	for(int i = 1; i <= t; i++)
		// 	{
		// 		var coll=GetSlideCollision(0);
		// 		var col=coll.GetCollider();
		// 		if(col is Node2D node && node.Name == "Bullet")
		// 		{
		// 			QueueFree();
		// 		}
		// 	}
		// }
	}
}
