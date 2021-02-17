extends TileMap

onready var unit_class = preload("res://scenes/units/Swordsman.tscn")

func _input(event):
	if event.is_action_pressed("ui_main_action"):
		var tile_pos = world_to_map(get_global_mouse_position())
		var val = get_cellv(tile_pos)
		print(tile_pos)
		var cell_size = Vector2(64.0, 64.0)
		if val == 1:
			var unit = unit_class.instance()
			var new_position = map_to_world(tile_pos)+cell_size/2
			unit.position = new_position
			get_parent().add_child(unit)

func _ready():
	pass
