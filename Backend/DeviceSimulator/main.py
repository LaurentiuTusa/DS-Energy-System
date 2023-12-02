import pika
import json
import time
import csv
from configparser import ConfigParser

def send_data_to_queue(channel, device_id, measurement_value):
    timestamp = int(time.time() * 1000)  # Convert current time to milliseconds

    message = {
        "timestamp": timestamp,
        "device_id": device_id,
        "measurement_value": measurement_value
    }

    # Convert the dictionary to a JSON-like string
    message_str = json.dumps(message)

    # Send the message to the RabbitMQ queue
    channel.basic_publish(exchange='', routing_key='energy_data', body=message_str)
    print(f" [x] Sent {message_str}")


def read_csv_file(file_path):
    with open(file_path, 'r') as file:
        reader = csv.reader(file)
        for row in reader:
            yield float(row[0])

def main():
    config = ConfigParser()
    config.read('config.ini')

    device_id = config.get('DeviceConfig', 'device_id')
    print("device_id: " + device_id)
    csv_file_path = "sensor.csv"

    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    channel = connection.channel()
    channel.queue_declare(queue='energy_data')

    for measurement_value in read_csv_file(csv_file_path):
        send_data_to_queue(channel, device_id, measurement_value)
        time.sleep(5)  # Simulate 5 seconds delay between measurements

    connection.close()

if __name__ == '__main__':
    main()