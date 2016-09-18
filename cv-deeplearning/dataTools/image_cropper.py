from PIL import Image
import os
from time import time

#root_path = './dataset/'
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
	for files in os.listdir(root_path + folders):
		img = Image.open(root_path + folders + files)
		width = img.size[0]
		height = img.size[1]

		half_width = img.size[0]/2
		half_height = img.size[1]/2

		img2 = img.crop(
			(
				half_width-20,
				half_height-30,
				half_width+40,
				half_height+35
			)
		)
		saveStr = "../images/" + folders + str(i) + ".jpg"
		img2.save(saveStr)
		i = i + 1
total_time = time() - t0
print 'Cropping time: ', total_time, 's'
