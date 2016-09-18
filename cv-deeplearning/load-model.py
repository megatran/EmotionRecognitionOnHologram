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

from sklearn.metrics import confusion_matrix

import imageDataExtract as dataset

# fix random seed for reproducibility
seed = 7
numpy.random.seed(seed)

# load data
matrix_path = 'numpy-matrix/main-0.npy'
label_path = 'numpy-matrix/label-0.npy'
X_train, y_train, X_test, y_test = dataset.load_matrix(matrix_path, label_path)


# normalize inputs from 0-255 to 0.0-1.0
X_train = X_train.astype('float32')
X_test = X_test.astype('float32')

print X_train.shape
print X_test.shape



X_train = X_train / 255.0
X_test = X_test / 255.0

# one hot encode outputs
y_train = np_utils.to_categorical(y_train)
y_test = np_utils.to_categorical(y_test)
num_classes = y_test.shape[1]

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


t0 = time()

#model.load_weights("models/model-0.h5")
model.load_weights("checkpoint/weights-improvement-75-0.1966-bigger.hdf5")

# Compile model

epochs = 25
lrate = 0.01
decay = lrate/epochs

sgd = SGD(lr=lrate, momentum=0.9, decay=decay, nesterov=False)

model.compile(loss='categorical_crossentropy', optimizer=sgd, metrics=['accuracy'])
print(model.summary())


print 'Loading time:'
total_time = time() - t0
print total_time, 's'


# Final evaluation of the model
scores = model.evaluate(X_test, y_test, verbose=0)
print("Accuracy: %.2f%%" % (scores[1]*100))

