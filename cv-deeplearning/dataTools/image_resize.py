from PIL import Image
import os
from time import time

new_width = 32
new_height = 32


root_path = '../images/'
slash = '/'
root = os.listdir(root_path)

print 'Iterating through folders:'

t0 = time()

# Iterating through the item directories to get dir
for folders in root:

	print folders

	folders = folders + slash

	i = 0
	for files in os.listdir(root_path + slash + folders):
		img = Image.open(root_path + folders + files)

		
		img2 = img.resize((new_width, new_height), Image.ANTIALIAS)

		saveStr = root_path + folders + files
		img2.save(saveStr)
		i = i + 1

total_time = time() - t0
print 'Resize time: ', total_time, 's'
