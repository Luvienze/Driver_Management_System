async function fetchDailyFemaleDriverCount() {
    try {
        const response = await fetch('/Driver/GetDailyFemaleDriverCount', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('totalFemaleDriversBox').textContent = count; 
    } catch (error) {
        console.error('Could not fetch female driver count:', error);
        document.getElementById('totalFemaleDriversBox').textContent = 'Error'; 
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDailyFemaleDriverCount(); 
});