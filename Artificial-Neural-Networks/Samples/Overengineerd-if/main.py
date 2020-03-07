from keras.models import Sequential
from keras.layers import Dense
from keras.optimizers import Adam
from keras import losses, activations, metrics
import random
import numpy as np

dataset = list()
answers = list()

for i in range(100000):
    value_x = random.random()
    value_y = random.random()
    dataset.append([value_x, value_y])
    if value_y > 0.5:
        answers.append([1])
    else:
        answers.append([0])

model = Sequential()
model.add(Dense(20, activation=activations.relu, input_dim=2))
model.add(Dense(1, activation=activations.sigmoid))
model.compile(optimizer=Adam(learning_rate=0.001), loss=losses.binary_crossentropy, metrics=[metrics.binary_accuracy])

model.fit(np.array(dataset), np.array(answers), 100, 200)
model.save_weights("model")