from keras import layers, models
from keras.datasets import cifar10
from sklearn.model_selection import ParameterGrid
import numpy as np
# import os
# os.environ["TF_ENABLE_ONEDNN_OPTS"] = "0"
(x_train, y_train), (x_test, y_test) = cifar10.load_data()

X = np.concatenate((x_train, x_test), axis=0)
Y = np.concatenate((y_train, y_test), axis=0)

indices = np.arange(len(X))
np.random.shuffle(indices)
X, Y = X[indices], Y[indices]

split_ratio = 0.3
split_point = int(len(X) * split_ratio)

x_train_new, x_test_new = X[:split_point], X[split_point:]
y_train_new, y_test_new = Y[:split_point], Y[split_point:]

print(f"Training data size: {x_train_new.shape[0]}")
print(f"Test data size: {x_test_new.shape[0]}")

x_train_new = x_train_new.astype('float32') / 255
x_test_new = x_test_new.astype('float32') / 255

animals = [2, 3, 4, 5, 6]
vehicles = [0, 1, 8, 9]
y_train_binary = np.where(np.isin(y_train_new, animals), 0, 1)
y_test_binary = np.where(np.isin(y_test_new, animals), 0, 1)

param_grid = {
    'conv_layers': [1, 2, 3],
    'filters': [32, 64],
    'dense_units': [64, 128],
    'batch_size': [32, 64],
    'epochs': [5, 10],
}

def create_model(conv_layers, filters, dense_units):
    model = models.Sequential()
    model.add(layers.Input(shape=(32, 32, 3)))
    model.add(layers.Conv2D(filters, (3, 3), activation='relu'))
    model.add(layers.MaxPooling2D((2, 2)))
    for _ in range(conv_layers - 1):
        model.add(layers.Conv2D(filters, (3, 3), activation='relu'))
        model.add(layers.MaxPooling2D((2, 2)))
    model.add(layers.Flatten())
    model.add(layers.Dense(dense_units, activation='relu'))
    model.add(layers.Dense(1, activation='sigmoid'))
    model.compile(optimizer='rmsprop', loss='binary_crossentropy', metrics=['accuracy'])
    return model

best_acc = 0
best_params = None

for params in ParameterGrid(param_grid):
    print(f"Testing with parameters: {params}")
    model = create_model(params['conv_layers'], params['filters'], params['dense_units'])
    history = model.fit(
        x_train_new, y_train_binary,
        epochs=params['epochs'], batch_size=params['batch_size'],
        validation_split=0.2, verbose=0
    )
    val_acc = max(history.history['val_accuracy'])
    print(f"Validation accuracy: {val_acc}")
    if val_acc > best_acc:
        best_acc = val_acc
        best_params = params

print(f"Best parameters: {best_params}")
print(f"Best validation accuracy: {best_acc}")

final_model = create_model(best_params['conv_layers'], best_params['filters'], best_params['dense_units'])
final_model.fit(x_train_new, y_train_binary, epochs=best_params['epochs'], batch_size=best_params['batch_size'])
test_loss, test_acc = final_model.evaluate(x_test_new, y_test_binary)
print(f"Test accuracy: {test_acc}")

