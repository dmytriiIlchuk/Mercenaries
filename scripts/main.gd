extends Node
export (PackedScene) var Swordsman
onready var file_manager_class = preload("res://scripts/utilities/file_manager.gd")

func _ready():
	randomize()
	var file_manager = file_manager_class.new("res://configuration1.cfg")
	file_manager.free()

func game_over():
	$ScoreTimer.stop()
	$MobTimer.stop()
	$HUD.show_game_over()

func new_game():
	$StartTimer.start()
