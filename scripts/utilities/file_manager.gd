var config_file : ConfigFile
var path : String

func _init(path: String): 
	# Initiate ConfigFile 
	config_file = ConfigFile.new()
	self.path = path
	
	var error = config_file.load(self.path)
 
	# Add values to file 
	config_file.set_value("Config","hostname","Server name") 
	config_file.set_value("Config","maxClients",32) 
	config_file.set_value("Options","difficulty","hard") 



func save_config(section: String, key: String):
	config_file.get_value(section, key)
