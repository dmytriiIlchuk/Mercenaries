extends Node

export (PackedScene) var Mob
export (PackedScene) var Player

func _ready():
	randomize()


func game_over():
	$ScoreTimer.stop()
	$MobTimer.stop()
	$HUD.show_game_over()

func new_game():
	$StartTimer.start()
	spawn_mobs()
	spawn_player($StartPosition.position)

func spawn_mobs():
	var start: float = 64.0
	var offset: float = 64.0
	for n in range(0,10):
		spawn_mob(Vector2(start + offset, 64.0))
		offset += start
	
func spawn_mob(position : Vector2):
	var mob = Mob.instance()
	add_child(mob)
	# Set the mob's position to a random location.
	mob.position = position

func spawn_player(position : Vector2):
	var player = Player.instance()
	add_child(player)
	# Set the mob's position to a random location.
	player.position = position
