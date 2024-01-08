import csv

def read_and_save_csv(input_file_path, base_output_file_path):
    try:
        with open(input_file_path, 'r') as input_file:
            # Create a CSV reader
            csv_reader = csv.reader(input_file)
            
            total_lines = 0
            file_index = 1
            
            while total_lines < 10:
                # Read the next two lines
                lines_to_read = min(2, 10 - total_lines)  # Read at most 2 lines or until a total of 10 lines are reached
                lines = [next(csv_reader) for _ in range(lines_to_read)]
                total_lines += lines_to_read
                
                # Generate the output file name
                output_file_path = f"{base_output_file_path}output_{file_index}.csv"
                
                # Open the output file for writing
                with open(output_file_path, 'w', newline='') as output_file:
                    # Create a CSV writer
                    csv_writer = csv.writer(output_file)
                    
                    # Write the lines to the output file
                    csv_writer.writerows(lines)
                
                print(f"Successfully saved {lines_to_read} lines to {output_file_path}")
                file_index += 1
            
    except FileNotFoundError:
        print(f"Error: File '{input_file_path}' not found.")
    except StopIteration:
        print("Error: Not enough lines in the input file.")

# Example usage:
# input_file_path = "D:/RawData/Books_rating.csv/Books_rating.csv"  # Replace with your input CSV file path
input_file_path = "../datafiles/netflix_titles.csv"  # Replace with your input CSV file path
base_output_file_path = '../outputHere/'  # Replace with your desired base output CSV file path

read_and_save_csv(input_file_path, base_output_file_path)
