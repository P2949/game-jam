using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

public partial class EvilMichaelEnglish : EnemyMovement
{
	[Export] public override float JumpVelocity { get; set; } = -200;
	[Export] public override int Speed { get; set; } = 40;
	[Export] public override int FallAcceleration { get; set; } = 200;
	
	
	public float damage { get; private set; } = 0;
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
	}
}
