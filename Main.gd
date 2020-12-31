extends Node
export (PackedScene) var Swordsman

func _ready():
	randomize()


func game_over():
	$ScoreTimer.stop()
	$MobTimer.stop()
	$HUD.show_game_over()

func new_game():
	$StartTimer.start()
