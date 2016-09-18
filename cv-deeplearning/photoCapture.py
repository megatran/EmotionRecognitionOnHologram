#from pyimagesearch import imutils
from matplotlib import pyplot as plt
import numpy as np
import math
import cv2
import imutils

face_classifier = cv2.CascadeClassifier('haarcascades/haarcascade_frontalface_default.xml')
cap = cv2.VideoCapture(0)

slash = '/'
underscore = '_'
dot = '.'

print 'Number of samples:'
iterations = raw_input()

print 'Name of folder to store:'
folder = raw_input()

print 'Name your file (without extension):'
file_name = raw_input()

print 'Name of extension:'
extension = raw_input()
print iterations
i = 0
while True:
    if i >= int(iterations):
        break
    
    print i
    ret, old_frame = cap.read()
    cv2.imshow('Video', old_frame)
    if cv2.waitKey(1) & 0xFF==ord('q'):
        break
    
    old_gray = cv2.cvtColor(old_frame, cv2.COLOR_BGR2GRAY)
    face = face_classifier.detectMultiScale(old_frame, 1.2, 4)

    if len(face) == 0:
        print "This is empty"
        continue
    else: 
        print 'Detected'
        for (x,y,w,h) in face:
            #focused_face = old_frame[y: y+h, x: x+w]
            focused_face = old_gray[y: y+h, x: x+w]
            cv2.rectangle(old_frame, (x,y), (x+w, y+h), (0,255,0),2)

        #cv2.imshow('Detected_Frame', focused_face)
        #cv2.waitKey(0)
        i_char = str(i)
        path = folder + slash + file_name + underscore + i_char + dot + extension
        cv2.imwrite(path,focused_face)
        i = i + 1

cap.release()
print 'Finished!'
