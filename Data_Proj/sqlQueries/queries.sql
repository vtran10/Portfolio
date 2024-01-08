-- path to db file "D:\RawData\DatasStores\newdb.db"

sqlite3

-- Inside the SQLite shell
CREATE TABLE IF NOT EXISTS students (
    id INTEGER PRIMARY KEY,
    name TEXT,
    age INTEGER
);

INSERT INTO students (name, age) VALUES
    ('John Doe', 20),
    ('Jane Smith', 22),
    ('Bob Johnson', 21),
    ('Chris Brown', 23);

SELECT * FROM students;



-- Exit the SQLite shell
-- .exit

