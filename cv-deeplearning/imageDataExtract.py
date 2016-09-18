from PIL import Image
from sklearn import cross_validation
import numpy as np
import os

def load_matrix_no_cross(matrix_path, label_path):
	main_ar = np.load(matrix_path)
	label = np.load(label_path)

	return main_ar, label

def load_matrix(matrix_path, label_path):
	main_ar = np.load(matrix_path)
	label = np.load(label_path)

	X_train, X_test, y_train, y_test = cross_validation.train_test_split(main_ar, label, test_size = 0.2, random_state=0)
        
	return X_train, y_train, X_test, y_test


def load_data():
	root_path = 'images/'
	slash = '/'
	root = os.listdir(root_path)

	print 'Iterating through folders'

	# Iterating through the item directories

	labelnum = 0
	i = 0
	for folders in root:
		print '-',folders


		folders = folders + slash
		
		j = 0
		for files in os.listdir(root_path + folders):
			imgO = Image.open(root_path + folders + files)
			img = np.array(imgO).transpose()

		
			if i == 0:
				# This is our first time with the image, so we initalize our main array
				main_ar = np.array([[img]])
				label = np.array([labelnum])
			else:
				# We will just concatenate the array then
				main_ar = np.concatenate(([[img]], main_ar))
				label = np.concatenate((label, [labelnum]))

			# Adding our label array
			i = i + 1
		labelnum = labelnum + 1


	# We have our main array and label array
	
	print 'Saving numpy arrays'

	# We are going to save our matrix and label array
	np.save('numpy-matrix/main.npy', main_ar)
	np.save('numpy-matrix/label.npy', label)
	
	print 'Successfully saved numpy arrays!'

	# Now we shall preform cross validation
	X_train, X_test, y_train, y_test = cross_validation.train_test_split(main_ar, label, test_size = 0.2, random_state=0)
	
	return X_train, y_train, X_test, y_test




def load_data_no_cross():
	root_path = 'images/'
	slash = '/'
	root = os.listdir(root_path)

	print 'Iterating through folders'

	# Iterating through the item directories

	labelnum = 0
	i = 0
	for folders in root:
		print '-',folders


		folders = folders + slash
		
		j = 0
		for files in os.listdir(root_path + folders):
			imgO = Image.open(root_path + folders + files)
			img = np.array(imgO).transpose()
		
		
			if i == 0:
				# This is our first time with the image, so we initalize our main array
				label = np.array([labelnum])

				print main_ar.shape
			else:
				# We will just concatenate the array then
				main_ar = np.concatenate(([img], main_ar))
				label = np.concatenate((label, [labelnum]))

			# Adding our label array
			i = i + 1
		labelnum = labelnum + 1


	# We have our main array and label array
	print 'Saving numpy arrays'

	# We are going to save our matrix and label array
	np.save('numpy-matrix/main.npy', main_ar)
	np.save('numpy-matrix/label.npy', label)
	
	print 'Successfully saved numpy arrays!'

	return main_ar, label

# To allow us to convert single image into a vector
def pathToVector(path):
	imgO = Image.open(path)
	return np.array([np.array([np.array(imgO).transpose()])])

