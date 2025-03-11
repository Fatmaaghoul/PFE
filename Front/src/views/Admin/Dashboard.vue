<template>
  <div class="dashboard">
    <!-- Stats Cards Row -->
    <div class="row mb-4">
      <div class="col-xl-3 col-md-6 mb-4">
        <div class="card stats-card bg-gradient-primary">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1"></h6>
                <h3 class="text-white mb-0"></h3>
                <small class="text-white-50"></small>
              </div>
              <div class="stats-icon">
                  <!-- <i class="bi bi-currency-dollar"></i> -->
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-3 col-md-6 mb-4">
        <div class="card stats-card bg-gradient-info">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1"></h6>
                <h3 class="text-white mb-0"></h3>
                <small class="text-white-50"></small>
              </div>
              <div class="stats-icon">
                  <!-- <i class="bi bi-cart"></i> -->
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-3 col-md-6 mb-4">
        <div class="card stats-card bg-gradient-success">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1"></h6>
                <h3 class="text-white mb-0"></h3>
                <small class="text-white-50"></small>
              </div>
              <div class="stats-icon">
                 <!--  <i class="bi bi-people"></i> -->
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-xl-3 col-md-6 mb-4">
        <div class="card stats-card bg-gradient-warning">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1"></h6>
                <h3 class="text-white mb-0"></h3>
                <small class="text-white-50"></small>
              </div>
              <div class="stats-icon">
                 <!--  <i class="bi bi-person-plus"></i> -->
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Charts Row -->
     
      
  </div>
</template>

<script>
export default {
  name: 'Dashboard'
}
</script>

<style scoped>
.stats-card {
  border: none;
  border-radius: 10px;
  transition: transform 0.2s;
}

.stats-card:hover {
  transform: translateY(-5px);
}

.bg-gradient-primary {
  background: linear-gradient(45deg, #4e73df 0%, #224abe 100%);
}

.bg-gradient-info {
  background: linear-gradient(45deg, #36b9cc 0%, #1a8a9c 100%);
}

.bg-gradient-success {
  background: linear-gradient(45deg, #1cc88a 0%, #13855c 100%);
}

.bg-gradient-warning {
  background: linear-gradient(45deg, #f6c23e 0%, #dda20a 100%);
}

.stats-icon {
  font-size: 2rem;
  opacity: 0.8;
  color: white;
}

.chart-area {
  height: 400px;
  position: relative;
}

.chart-placeholder {
  height: 100%;
  display: flex;
  align-items: flex-end;
  padding: 20px;
}

.chart-bars {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  width: 100%;
  height: 100%;
}

.chart-bar {
  width: 40px;
  background: linear-gradient(45deg, #4e73df 0%, #224abe 100%);
  border-radius: 5px;
  transition: height 0.3s ease;
}

.chart-pie {
  height: 300px;
  position: relative;
}

.pie-placeholder {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
}

.pie-segment {
  position: absolute;
  width: 200px;
  height: 200px;
  border-radius: 50%;
  background: conic-gradient(
    var(--color) 0% var(--percentage),
    transparent var(--percentage) 100%
  );
}

.pie-label {
  position: absolute;
  bottom: -30px;
  left: 50%;
  transform: translateX(-50%);
  white-space: nowrap;
  font-size: 0.8rem;
  color: #6c757d;
}

.card {
  margin-bottom: 1.5rem;
}

.font-weight-bold {
  font-weight: 700;
}

.text-primary {
  color: #4e73df !important;
}
</style> 