import csv
import sqlite3

def read_and_save_to_sqlite(input_file_path, db_file_path):
    try:
        conn = sqlite3.connect(db_file_path)
        cursor = conn.cursor()

        with open(input_file_path, 'r', encoding='utf-8') as input_file:
            # Create a CSV reader
            csv_reader = csv.reader(input_file)

            # Read the header to get column names
            header = next(csv_reader)

            # Create a table in the database with explicitly defined columns and types
            create_table_query = f'''CREATE TABLE IF NOT EXISTS output_data (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        show_id varchar2,
                                        type varchar2,
                                        title varchar2,
                                        director varchar2,
                                        cast varchar2,
                                        country varchar2,
                                        date_added varchar2,
                                        release_year varchar2,
                                        rating varchar2,
                                        duration varchar2,
                                        listed_in varchar2,
                                        description varchar2
                                    )'''
            cursor.execute(create_table_query)

            total_lines = 0
            for row in csv_reader:
                # Convert each row to a tuple
                row_tuple = tuple(row)

                # Insert the row into the database
                cursor.execute(f'''
                    INSERT INTO output_data 
                    ({", ".join(header)})
                    VALUES ({", ".join(["?"] * len(row))})
                ''', row_tuple)

                total_lines += 1

        # Commit the changes and close the connection
        conn.commit()
        conn.close()

        print(f"Successfully saved {total_lines} lines to {db_file_path}")

    except FileNotFoundError:
        print(f"Error: File '{input_file_path}' not found.")
    except StopIteration:
        print("Error: Not enough lines in the input file.")
    except Exception as e:
        print(f"An error occurred: {e}")

# Example usage:
input_file_path = '../datafiles/netflix_titles.csv'  # Replace with your input CSV file path
db_file_path = 'D:/RawData/DatasStores/movies2.db'  # Replace with your desired SQLite database file path

read_and_save_to_sqlite(input_file_path, db_file_path)
