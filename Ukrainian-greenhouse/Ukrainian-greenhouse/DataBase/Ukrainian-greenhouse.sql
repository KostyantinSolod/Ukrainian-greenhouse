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
    energy_consumed TIMESTAMP NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
CREATE TABLE greenhouse_monitoring (
    id SERIAL PRIMARY KEY,
    list_id INT,
    timestamp_report TIMESTAMP NOT NULL,
    timestamp_information TIMESTAMP NOT NULL,
    temperature_information REAL NOT NULL,
    humidity_information REAL NOT NULL,
    irrigation_time_information TIMESTAMP NOT NULL,
    irrigation_volume_information REAL NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
create table list (
	list_id SERIAL PRIMARY KEY,
	name_of_culture VARCHAR(25)
);
CREATE TABLE watering_schedule (
    watering_id SERIAL PRIMARY KEY,
    list_id INT,
    irrigation_time TIMESTAMP NOT NULL,
    irrigation_volume REAL NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
						