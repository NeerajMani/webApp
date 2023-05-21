import tkinter as tk
from tkinter import filedialog
from tkinter import messagebox
from datetime import datetime
import asyncio
from pyppeteer import launch
import docx

# Function to take a screenshot of the graph
async def take_screenshot(page):
    graph_element = await page.waitForSelector("selector-for-graph-element")
    graph_box = await graph_element.boundingBox()
    screenshot_path = f"{api_name}.png"
    await page.screenshot(path=screenshot_path, clip=graph_box)
    return screenshot_path

# Function to update the Word document with the graph
def update_word_document(api_name, graph_image, word_doc):
    word_doc.add_paragraph(api_name)
    word_doc.add_picture(graph_image)

# Function to handle the "Select Document" button click event
def select_document():
    word_doc_path = filedialog.askopenfilename(filetypes=[("Word Documents", "*.docx")])
    word_doc_entry.delete(0, tk.END)
    word_doc_entry.insert(0, word_doc_path)

# Function to handle the "Update" button click event
async def update_graphs():
    api_names = api_names_entry.get().split(",")
    domain_name = domain_name_entry.get()
    start_date_str = start_date_entry.get()
    end_date_str = end_date_entry.get()
    word_doc_path = word_doc_entry.get()

    # Convert start and end dates to milliseconds
    start_date = int(datetime.strptime(start_date_str, "%Y-%m-%d %H:%M").timestamp() * 1000)
    end_date = int(datetime.strptime(end_date_str, "%Y-%m-%d %H:%M").timestamp() * 1000)

    # Launch Puppeteer
    async def run():
        browser = await launch()
        page = await browser.newPage()

        # Open Splunk in the same tab
        await page.goto("https://www.splunk.com", {"waitUntil": "networkidle0"})

        # Run queries and update Word document
        word_doc = docx.Document(word_doc_path)
        for api_name in api_names:
            # Build the URL with API name, domain, start date, and end date
            url = f"https://www.splunk.com/{api_name}/{domain_name}?start={start_date}&end={end_date}"
            await page.goto(url, {"waitUntil": "networkidle0"})

            # Take a screenshot of the graph
            graph_image_path_cpu = await take_screenshot(page)

            # Update the Word document with the API name and graph
            update_word_document(api_name, graph_image_path_cpu, word_doc)

            url = f"https://www.splunk.com/{api_name}/{domain_name}?start={start_date}&end={end_date}"
            await page.goto(url, {"waitUntil": "networkidle0"})

            # Take a screenshot of the graph
            graph_image_path_mem = await take_screenshot(page)

            # Update the Word document with the API name and graph
            update_word_document(api_name, graph_image_path_mem, word_doc)

        # Save and close the Word document
        word_doc.save(word_doc_path)
        word_doc.close()

        # Close the browser
        await browser.close()

    # Run the asyncio event loop
    await asyncio.get_event_loop().run_until_complete(run())

    messagebox.showinfo("Update Completed", "Graphs updated successfully.")

# Create the main window
window = tk.Tk()
window.title("Graph Update Utility")

# Create UI elements
api_names_label = tk.Label(window, text="API Names:")
api_names_entry = tk.Entry(window)
domain_name_label = tk.Label(window, text="Domain Name:")
domain_name_entry = tk.Entry(window)
start_date_label = tk.Label(window, text="Start Date (YYYY-MM-DD HH:MM):")
start_date_entry = tk.Entry(window)
end_date_label = tk.Label(window, text="End Date (YYYY-MM-DD HH:MM):")
end_date_entry = tk.Entry(window)
word_doc_label = tk.Label(window, text="Word Document:")
word_doc_entry = tk.Entry(window)
word_doc_button = tk.Button(window, text="Select Document", command=select_document)
update_button = tk.Button(window, text="Update Graphs", command=update_graphs)

# Layout UI elements
api_names_label.grid(row=0, column=0, padx=10, pady=10)
api_names_entry.grid(row=0, column=1, padx=10, pady=10)
domain_name_label.grid(row=1, column=0, padx=10, pady=10)
domain_name_entry.grid(row=1, column=1, padx=10, pady=10)
start_date_label.grid(row=2, column=0, padx=10, pady=10)
start_date_entry.grid(row=2, column=1, padx=10, pady=10)
end_date_label.grid(row=3, column=0, padx=10, pady=10)
end_date_entry.grid(row=3, column=1, padx=10, pady=10)
word_doc_label.grid(row=4, column=0, padx=10, pady=10)
word_doc_entry.grid(row=4, column=1, padx=10, pady=10)
word_doc_button.grid(row=4, column=2, padx=10, pady=10)
update_button.grid(row=5, column=0, columnspan=3, padx=10, pady=10)

# Run the main event loop
window.mainloop()

