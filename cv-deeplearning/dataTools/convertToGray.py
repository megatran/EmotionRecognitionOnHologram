import os
from time import time
import cv2

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
		img = cv2.imread(root_path + folders + files)
		img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
		

		saveStr = "../images/" + folders + str(i) + ".jpg"
		cv2.imwrite(saveStr, img)
		i = i + 1

total_time = time() - t0
print 'Convert time: ', total_time, 's'
