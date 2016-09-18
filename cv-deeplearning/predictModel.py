# Simple CNN model for CIFAR-10
import numpy
import matplotlib.pyplot as plt
from time import time

from keras.models import Sequential
from keras.layers import Dense
from keras.layers import Dropout
from keras.layers import Flatten
from keras.constraints import maxnorm
from keras.optimizers import SGD
from keras.layers.convolutional import Convolution2D
from keras.layers.convolutional import MaxPooling2D
from keras.utils import np_utils

#from sklearn.metrics import confusion_matrix

import imageDataExtract as dataset

import requests

# For recording
import cv2
from PIL import Image

# fix random seed for reproducibility
seed = 7
numpy.random.seed(seed)
mode = 'v'

num_classes = 3

output = ['Sad','Happy','Angry']


# Create the model
model = Sequential()
model.add(Convolution2D(32, 3, 3, input_shape=(1, 32, 32), border_mode='same', activation='relu', W_constraint=maxnorm(3)))
model.add(Dropout(0.2))
model.add(Convolution2D(32, 3, 3, activation='relu', border_mode='same', W_constraint=maxnorm(3)))
model.add(MaxPooling2D(pool_size=(2, 2)))
model.add(Flatten())
model.add(Dense(512, activation='relu', W_constraint=maxnorm(3)))
model.add(Dropout(0.5))
model.add(Dense(num_classes, activation='softmax'))



model.load_weights("models/model-0.h5")

# Compile model

epochs = 25
lrate = 0.01
decay = lrate/epochs

sgd = SGD(lr=lrate, momentum=0.9, decay=decay, nesterov=False)

model.compile(loss='categorical_crossentropy', optimizer=sgd, metrics=['accuracy'])


if mode == 'v':

	tar_height = 32
	tar_width = 32


	face_classifier = cv2.CascadeClassifier('haarcascades/haarcascade_frontalface_default.xml')
	cap = cv2.VideoCapture(0)

	i = 0

	while True:
		print 'Press enter to start recording: '
		temp = raw_input()


		while True:

		    ret, old_frame = cap.read()

		    '''
		    cv2.imshow('Video', old_frame)
		    if cv2.waitKey(1) & 0xFF==ord('q'):
			break
		    '''


		    old_gray = cv2.cvtColor(old_frame, cv2.COLOR_BGR2GRAY)
		    face = face_classifier.detectMultiScale(old_frame, 1.2, 4)

		    if len(face) == 0:
			continue
		    else:
			print 'Detected'
			for (x,y,w,h) in face:
			    #focused_face = old_frame[y: y+h, x: x+w]
			    img = old_gray[y: y+h, x: x+w]
			    #cv2.rectangle(old_frame, (x,y), (x+w, y+h), (0,255,0),2)
			    pil_im = Image.fromarray(img)
			    img2 = pil_im.resize((32, 32), Image.ANTIALIAS)

			    img2.save('1.png')

			    cvimg = numpy.array(img2.convert('RGB'))
			    img = cv2.cvtColor(cvimg[:, :, ::-1].copy(), cv2.COLOR_BGR2GRAY)

			    print img.shape

			    img = numpy.array([numpy.array([img])])


			    # normalize inputs from 0-255 to 0.0-1.0
			    img = img.astype('float32')


			    # Final evaluation of the model

			    if i%10 == 0:
			        json = open("./test.json", "w")
			        print 'JSON written'

			    pred = model.predict_classes(img, 1, verbose=0)

			    print i, ' ', output[pred[0]]
			    print ''
			    print ''
			    print ''

			    requests.post('http://5b568fb2.ngrok.io/emotion', data = ('{"classification": "%s", "level": %d}' % (output[pred[0]], 10)))
			    strTemp = '{\n "classification": "' + output[pred[0]] + '"\n}\n'
			    if i%10 == 0:
			        json.write(strTemp)
			        json.close()
			    i = i + 1
