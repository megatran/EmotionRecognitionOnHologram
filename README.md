<p align = "center">
    <img src="https://challengepost-s3-challengepost.netdna-ssl.com/photos/production/software_photos/000/411/009/datas/gallery.jpg" />
</p>

<br>

A project in Hack the North 2016--Canada's biggest hackathon

A system that can recognize human facial expression and analyze emotions (i.e: happy, sad, angry...) using CV and Machine Learning. After that, the human emotion data is streamed to a Unity character which is projected on a DIY pyramid hologram.

Team: Nhan Tran, Jacob Emmel, and Anthony Lowhur

## Building & Running

### Requirements

* Unity Personal Edition (developed with v5.4)
* Python 2.7
 * [Requests](http://docs.python-requests.org/en/master/user/install/)  
 * [Numpy](http://docs.scipy.org/doc/numpy/user/install.html)
 * [matplotlib](http://matplotlib.org/users/installing.html)
 * [scikit-learn](http://scikit-learn.org/stable/install.html)
 * [h5py](http://docs.h5py.org/en/latest/build.html)
 * [Keras](https://keras.io/#installation)

All Python dependencies can be installed with pip:

```
pip install requests numpy scikit-learn matplotlib h5py keras
```

### Run

A windowed executable for the Unity project (including the web server) can be created through _File_ > _Build & Run_. The project can also be run by clicking the play button in the upper middle part of the Unity screen.

The Python script, which takes input through the webcam and sends emotion information to the backend service, can be run with the following (from the current directory):

```
python cv-deeplearning/predictModel.py
```
