CREATE TABLE Users(
	id SERIAL PRIMARY KEY,
	login TEXT NOT NULL,
	password TEXT NOT NULL
);
create table list (
	list_id SERIAL PRIMARY KEY,
	name_of_culture VARCHAR(25)
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
    energy_consumed TIMESTAMP NOT NULL,
    number_of_lamps integer NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id),
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
    energy_consumed TIMESTAMP NOT NULL,
    number_of_lamps int NOT NULL,
    total_energy_consumed double precision NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
CREATE TABLE watering_schedule (
    watering_id SERIAL PRIMARY KEY,
    list_id INT,
    irrigation_time TIMESTAMP NOT NULL,
    irrigation_volume REAL NOT NULL,
    FOREIGN KEY (list_id) REFERENCES list(list_id)
);
CREATE table list_lamp (
	lamp_id SERIAL PRIMARY KEY,
	name_of_lamp VARCHAR (50),
	power_use double precision
);
INSERT INTO list_lamp
VALUES ( 1, 'світлодіодні', '0.0125'),
( 2, 'люмінесцентні', '0.019'),
( 3, 'натрієві', '0.011'),
( 4, 'ртутні', '0.145'),
( 5, 'металогалогенні', '0.15'),
( 6, 'інфрачервоні', '0.175'),
( 7, 'потужні лампи розжарювання', '0.075')