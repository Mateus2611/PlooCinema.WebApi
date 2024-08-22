CREATE TABLE movie (
id SERIAL PRIMARY KEY NOT NULL,
name VARCHAR(255) NOT NULL,
description VARCHAR(255) NOT NULL,
duration INT NOT NULL,
release DATE NOT NUll
);

CREATE TABLE genre (
id SERIAL PRIMARY KEY NOT NULL,
name VARCHAR(255) NOT NULL
);

CREATE TABLE movie_genre (
movie_id INT NOT NULL,
genre_id INT NOT NULL,
FOREIGN KEY (movie_id) REFERENCES movie(id),
FOREIGN KEY (genre_id) REFERENCES genre(id), 
PRIMARY KEY (movie_id, genre_id)
);

CREATE TABLE room (
id SERIAL PRIMARY KEY NOT NULL,
name VARCHAR(55) NOT NULL,
seats INT NOT NULL
);

CREATE TABLE session (
id SERIAL PRIMARY KEY NOT NULL,
movie_id INT NOT NULL,
room_id INT NOT NULL,
movie_start TIMESTAMPTZ NOT NULL,
available_seats INT NOT NULL,
FOREIGN KEY (movie_id) REFERENCES movie(id),
FOREIGN KEY (room_id) REFERENCES room(id)
);