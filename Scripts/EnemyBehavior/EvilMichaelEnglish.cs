using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

public partial class EvilMichaelEnglish : EnemyBehavior
{
	[Export] public override float jumpVelocity { get; set; } = -300;
	[Export] public override int speed { get; set; } = 100;
	[Export] public override int fallAcceleration { get; set; } = 300;
	[Export] public override int maxHealth {get; set;} = 5;
	
	public override float touchDamage { get; set; } = 5;
	bool alive { get; } = true;
	
	public async void MovementLoop() {
		Random random = new Random();
		while (alive) {
			
			if ((random.NextSingle() * 10) > 6) Jump();
			
			await MoveX(9000);
			
			if ((random.NextSingle() * 10) > 6) Jump();
			
			await MoveX(-9000);
		}
	}
	
	public override void _Ready() {
		MovementLoop();
		GD.Print("michael");
	}
}
