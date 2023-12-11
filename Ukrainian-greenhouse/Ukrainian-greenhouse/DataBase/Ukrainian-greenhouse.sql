CREATE TABLE Users(
	id SERIAL PRIMARY KEY,
	login TEXT NOT NULL,
	email TEXT NOT NULL,
	password TEXT NOT NULL,
	role INTEGER NOT NULL
);
create table climate_control (
    id SERIAL PRIMARY KEY,
    list_id INT,
    timestamp TIMESTAMP NOT NULL,
    temperature REAL NOT NULL,
    humidity REAL NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
CREATE TABLE energy_management (
    energy_id SERIAL PRIMARY KEY,
    list_id INT,
    electricity BOOLEAN NOT NULL,
    power_consumption REAL NOT NULL,
    energy_consumed TIMESTAMP NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);CREATE TABLE greenhouse_monitoring (
    report_id serial primary key,
    list_id INT,

    timestamp_information TIMESTAMP NOT NULL,
    temperature_information REAL NOT NULL,
    humidity_information REAL NOT NULL,
	
    plant_type_information VARCHAR(255) NOT NULL,
    watering_schedule_information JSONB NOT NULL,
    soil_moisture_level_information REAL NOT NULL,
    irrigation_time_information TIMESTAMP NOT NULL,
    irrigation_volume_information REAL NOT NULL,
	
    power_consumption_information REAL NOT NULL,
    energy_consumed_information TIMESTAMP NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
create table list (
	list_id SERIAL PRIMARY KEY,
	name_of_culture VARCHAR(25)
);

INSERT INTO list VALUES (1, 'Potato'),
						(2, 'Tomato'),
						(3, 'Konoplya');

CREATE TABLE watering_schedule (
    watering_id SERIAL PRIMARY KEY,
    list_id INT,
    plant_type VARCHAR(255) NOT NULL,
    watering_schedule JSONB NOT NULL,
    soil_moisture_level REAL NOT NULL,
    irrigation_time TIMESTAMP NOT NULL,
    irrigation_volume REAL NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
						