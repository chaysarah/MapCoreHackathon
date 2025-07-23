import os
import glob
import re
import tkinter as tk
from tkinter import messagebox
from datetime import datetime

def update_html(selected_relative_js):
    if not os.path.exists(html_file):
        messagebox.showerror("Error", f"File {html_file} does not exist!")
        return

    # Read the current HTML content.
    with open(html_file, "r", encoding="utf-8") as file:
        content = file.read()

    # Regex pattern to find a script tag with a src attribute that optionally starts with "bin/"f
    # and then a file that starts with "MapCore_" and ends with ".js".
    pattern = r'(<script\s+src=")(?:bin/[^"]*/MapCore[^"]*\.js|MapCore(?:-[^"]+)?\.js)(".*?></script>)'
    match = re.search(pattern, content, re.IGNORECASE)
    if not match:
        messagebox.showerror("Error", "No script tag with a MapCore*.js source found in the HTML file!")
        return

    # Convert any backslashes in the selected path to forward slashes.
    selected_relative_js = selected_relative_js.replace("\\", "/")
    # Prepend "bin/" to the selected relative path.
    new_src = f'bin/{selected_relative_js}'
    new_content = re.sub(pattern, rf'\1{new_src}\2', content)

    # Write the updated content back to the HTML file.
    with open(html_file, "w", encoding="utf-8") as file:
        file.write(new_content)
    
    messagebox.showinfo("Success", f"Updated {html_file} to use {new_src}")

def on_select(event):
    widget = event.widget
    selection = widget.curselection()
    if selection:
        index = selection[0]
        # Retrieve the original relative path from our file_data list.
        selected_relative = file_data[index][0]
        update_html(selected_relative)
        root.destroy()

# Check if the symlink to Emscripten bin already exists
target = os.path.join("..", "..", "..", "bin", "Emscripten") #"../../../bin/Emscripten"
symlink = "bin"

if not os.path.islink(symlink):
    os.symlink(target, symlink, target_is_directory=True)
    print(f"Symlink created: {symlink} -> {target}")
else:
    print(f"Symlink already exists: {symlink}")

# Choose html application
def list_html_files():
    """Lists all .html files in the current directory."""
    html_files = [f for f in os.listdir() if f.endswith(".html")]
    return html_files

def choose_html_file(html_files):
    """Prompts the user to choose an HTML file interactively."""
    if not html_files:
        print("No HTML files found in the current directory.")
        return None

    print("\nAvailable HTML files:")
    for index, file in enumerate(html_files, start=1):
        print(f"{index}. {file}")

    while True:
        try:
            choice = int(input("\nEnter the number of the HTML file to choose: ")) - 1
            if 0 <= choice < len(html_files):
                return html_files[choice]
            else:
                print("Invalid choice. Please enter a valid number.")
        except ValueError:
            print("Invalid input. Please enter a number.")

# Main execution
html_files = list_html_files()
html_file = choose_html_file(html_files)

if html_file:
    print(f"\nYou selected: {html_file}")


# Search recursively for JavaScript files under the "bin" folder.
js_files = glob.glob(os.path.join("bin", "**", "MapCore*.js"), recursive=True)

# Filter out files that end with ".worker.js".
js_files = [js for js in js_files if not js.endswith(".worker.js")]

if not js_files:
    print("No MapCore*.js files (excluding .worker.js) found in the ./bin folder.")
    exit(1)

# Build a list of tuples: (relative_path, display_string).
# The display string includes the relative path (from bin) and the file's last modified date.
file_data = []
for js_file in js_files:
    # Get the path relative to "bin" (excluding the "bin" prefix).
    rel_path = os.path.relpath(js_file, "bin")
    mod_time = os.path.getmtime(js_file)
    mod_date = datetime.fromtimestamp(mod_time).strftime("%Y-%m-%d %H:%M:%S")
    display_str = f"{rel_path} - {mod_date}"
    file_data.append((rel_path, display_str))

# Create the main Tkinter window.
root = tk.Tk()
root.title("Select a JS File")

listbox = tk.Listbox(root, width=80, height=15)
for _, display_str in file_data:
    listbox.insert(tk.END, display_str)
listbox.pack(padx=20, pady=20)

# Bind the selection event to update the HTML file.
listbox.bind('<<ListboxSelect>>', on_select)

# Run the Tkinter main event loop.
root.mainloop()
