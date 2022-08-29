<template>
  <div>
    <vue-final-modal v-model="showModal" classes="modal-container" content-class="modal-content" 
    @closed="closed"
    @cancel="showModal = false"
    :transition="{
        'enter-active-class': 'transition duration-[400ms] ease-in-out transform',
        'enter-from-class': 'scale-y-0 ',
        'enter-to-class': 'scale-y-100',
        'leave-active-class': 'transition duration-[400ms] ease-in-out transform',
        'leave-to-class': 'scale-y-0',
        'leave-from-class': 'scale-y-100'
      }"
    >
      <vuln-modal-content :type="this.contentComponent" :fetchingId="this.fetchingId"/>
      <weakness-modal-content :type="this.contentComponent" :fetchingId="this.fetchingId"/>
    </vue-final-modal>
  </div>
</template>

<script>
import { VueFinalModal, ModalsContainer } from 'vue-final-modal'
import VulnModalContent from '@/components/Topbar/VulnModalContent.vue'
import WeaknessModalContent from '@/components/Topbar/WeaknessModalContent.vue'

export default {
  name: "ModalComponent",
  components: {
    VueFinalModal,
    ModalsContainer,
    VulnModalContent,
    WeaknessModalContent
  },
  props: {
    contentComponent: {
      type: String,
      default: null
    },
    fetchingId : {
      type: String,
      default: null
    }
  },
  data: () => ({
    showModal: false
  }),
  watch: {
    fetchingId(newId) {
      if (newId !== null) {
        this.showModal = true
      }
    }
  },
  methods: {
    closed() {
      this.$emit('clear:fetchingId', null)
    }
  },
}
</script>

<style scoped>
:deep(.modal-container)  {
  display: flex;
  justify-content: center;
  align-items: center;
  @apply h-full
}
:deep(.modal-content)  {
  display: flex;
  flex-direction: column;
  margin: 0 1rem;
  background: #fff;
  @apply h-5/6 w-10/12 shadow-2xl shadow-gray-400/90
}
.modal__title {
  font-size: 1.5rem;
  font-weight: 700;
}
</style>

<style scoped>
.dark-mode div:deep(.modal-content) {
  border-color: #2d3748;
  background-color: #1a202c;
}
</style>