import requests
from faker import Faker
import time
from requests.exceptions import ConnectionError

# URL of the API endpoint
URL = "https://localhost:7032/api/members"

# Initialize Faker
fake = Faker()

# Function to generate random data
def generate_random_data():
    return {
        "name": fake.name(),
        "email": fake.email(),
        "phone": fake.phone_number(),
        "address": fake.address(),
        "subscriptionPlan": fake.random_element(elements=("Monthly", "Trimestral", "Semi-annual", "Annual")),
        "membershipStartDate": fake.date_this_year().isoformat(),
        "membershipEndDate": fake.date_this_year().isoformat()
    }

# Loop to create 50 members
for i in range(1, 1001):
    data = generate_random_data()
    try:
        response = requests.post(URL, json=data, verify=False)
        if response.status_code == 201:
            print(f"Added Member {i}: {data['name']}")
        else:
            print(f"Failed to add Member {i}: {response.status_code} - {response.text}")
    except ConnectionError as e:
        print(f"Connection error while adding Member {i}: {e}")
        time.sleep(5)  # Wait for 5 seconds before retrying
        try:
            response = requests.post(URL, json=data, verify=False)
            if response.status_code == 201:
                print(f"Added Member {i}: {data['name']}")
            else:
                print(f"Failed to add Member {i}: {response.status_code} - {response.text}")
        except ConnectionError as e:
            print(f"Second attempt failed for Member {i}: {e}")