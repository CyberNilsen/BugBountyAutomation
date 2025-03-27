import sys
import subprocess
import os


DIRSEARCH_PATH = os.path.join(os.path.dirname(__file__), "..", "Tools", "dirsearch", "dirsearch.py")

def run_dirsearch(target):
    if not os.path.exists(DIRSEARCH_PATH):
        print("[-] Error: Dirsearch not found!")
        return

    try:
        print(f"[+] Scanning {target} with Dirsearch...\n")
        
       
        result = subprocess.run(["python", DIRSEARCH_PATH, "-u", target, "-e", "js,php,html,txt"], capture_output=True, text=True)

        print(result.stdout)
    except Exception as e:
        print(f"[-] Error: {e}")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("[-] No target provided!")
        sys.exit(1)

    target_url = sys.argv[1]
    run_dirsearch(target_url)
