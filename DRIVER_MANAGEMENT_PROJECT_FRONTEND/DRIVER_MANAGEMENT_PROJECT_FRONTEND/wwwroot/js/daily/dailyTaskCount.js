async function fetchDailyTaskCount() {
    try {
        const response = await fetch('/Task/GetDailyTaskCount', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API hatası: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('dailyTaskCountBox').textContent = count; 
    } catch (error) {
        console.error('Failed at fetch task count:', error);
        document.getElementById('dailyTaskCountBox').textContent = 'Error';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDailyTaskCount(); 
});