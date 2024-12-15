import requests
from faker import Faker
import time
from requests.exceptions import ConnectionError

# URL of the API endpoint
URL = "https://localhost:7032/api/trainers"

# Initialize Faker
fake = Faker()

# Function to generate random data
def generate_random_data():
    return {
        "name": fake.name(),
        "email": fake.email(),
        "phone": fake.phone_number(),
        "address": fake.address(),
        "joiningDate": fake.date_this_decade().isoformat(),
        "salary": round(fake.random_number(digits=5), 2),
        "classes": []
    }

# Loop to create 100 trainers
for i in range(1, 101):
    data = generate_random_data()
    try:
        response = requests.post(URL, json=data, verify=False)
        if response.status_code == 201:
            print(f"Added Trainer {i}: {data['name']}")
        else:
            print(f"Failed to add Trainer {i}: {response.status_code} - {response.text}")
    except ConnectionError as e:
        print(f"Connection error while adding Trainer {i}: {e}")
        time.sleep(5)  # Wait for 5 seconds before retrying
        try:
            response = requests.post(URL, json=data, verify=False)
            if response.status_code == 201:
                print(f"Added Trainer {i}: {data['name']}")
            else:
                print(f"Failed to add Trainer {i}: {response.status_code} - {response.text}")
        except ConnectionError as e:
            print(f"Second attempt failed for Trainer {i}: {e}")